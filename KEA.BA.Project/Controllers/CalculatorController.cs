using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEA.BA.Project.Controllers
{
    public class CalculatorController
    {
        public int CalculateGroupSizes(int storeSize, int groupDistance)
        {
            int groupSize = storeSize / (groupDistance*groupDistance);

            return groupSize;
        }

        public void CreateShoppingGroups()
        {

        }

        public void PopulateGroups()
        {

        }
    }
}