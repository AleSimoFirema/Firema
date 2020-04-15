using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainBenchSimulationSW
{
    
    public class ScriptingManager
    {
        public ScriptingManager()
        {
        }
        public static void openScript(Microsoft.Office.Interop.Excel.Range xlRange, ObservableCollection<scriptRow.Dati> scriptData)
        {
            ObservableCollection<scriptRow.Dati> datiBackUp = new ObservableCollection<scriptRow.Dati>();
            int xlRow;
            int i = 0;
            for (xlRow = 2; xlRow < xlRange.Count; xlRow++)
            {
                if (xlRange.Cells[xlRow, 1].Text != "")
                {
                    i++;
                    scriptData.Add(new scriptRow.Dati { operation = xlRange.Cells[xlRow, 1].Text, name = xlRange.Cells[xlRow, 2].Text, value = Convert.ToDouble(xlRange.Cells[xlRow, 3].Text) });
                }
            }
            //return dataScript;
        }

    }
}
