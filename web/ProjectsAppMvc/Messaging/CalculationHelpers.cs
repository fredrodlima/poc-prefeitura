using ProjectsAppMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectsAppMvc.Messaging
{
    public static class CalculationHelpers
    {
        public static double CalculateProgress(IEnumerable<ProjectPhase> projectPhases)
        {
            if (projectPhases.Count() == 0)
            {
                return 0.0;
            }
            var progress = 0.0;
            foreach (var phase in projectPhases)
            {
                switch (phase.Status)
                {
                    case PhaseStatus.NotStarted:
                        {
                            progress += 0;
                            break;
                        }
                    case PhaseStatus.InProgress:
                        {
                            progress += 0.5;
                            break;
                        }
                    case PhaseStatus.Completed:
                        {
                            progress += 1;
                            break;
                        }
                }
            }
            return progress / projectPhases.Count();
        }
    }
}
