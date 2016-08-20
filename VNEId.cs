using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visual_Novel_Engine
{
    public class VNEID
    {
        public string ScenedId;
        public string LineId;
        public string OptionId;
        
        public static implicit operator string(VNEID vneid)
        {
            return vneid.ToString();
        }
        public static implicit operator VNEID(string vneidstring)
        {
            string[] parts = new string[3];
            parts = vneidstring.Split('.');
            
            
            string scenedId="";
            string lineId="";
            string optionId="";

            if (parts.Length==1)
                scenedId = parts[0];
            if (parts.Length==2)
                lineId = parts[1];
            if (parts.Length==3)
                optionId = parts[2];


            return new VNEID(scenedId, lineId, optionId);
            
        }

        public VNEID(string scenedId, string lineId = null, string optionId = null)
        {
            this.ScenedId = scenedId;
            this.LineId = lineId;
            this.OptionId = optionId;
        }
   
        public override string ToString()
        {
            string IdString = "";
            if (!string.IsNullOrEmpty(ScenedId))
            {
                IdString += ScenedId;
                if (!string.IsNullOrEmpty(LineId))
                {
                    IdString = IdString + "." + LineId;

                    if (!string.IsNullOrEmpty(OptionId))
                    {
                        IdString = IdString + "." + OptionId;
                    }
                }
            }
            
            return IdString;
        }
    }
}
