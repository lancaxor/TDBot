using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TDBot
{
    class ConfigWorker
    {
        List<Parameter> pars;   //params
        int parc;           //param count
        String path = Directory.GetCurrentDirectory() + "\\config.inf";

        public ConfigWorker()
        {
            this.parc = 0;
            pars = new List<Parameter>();
            ReloadConfig();
        }

        public void ReloadConfig()      //config to mem; dispose any nonsaved changes
        {
            int index = 0;
            String[] rawPars;

            if (!File.Exists(path))
            {
                File.Create(path).Close();
                parc = 0;
                return;
            }
            rawPars = File.ReadAllLines(path);
            if (rawPars.Length > 0)
            {
                foreach (String par in rawPars)
                {
                    Parameter param = new Parameter(par);
                    if (!ParameterExists(param))
                    {
                        pars.Add(new Parameter(par.ToLower()));
                        parc++;
                        index++;
                    }
                    else
                    {
                        SetConfig(param.GetName(), param.GetValue());
                    }
                }
            }
        }
        
        public string[] getAllConfig()
        {
            //ReloadConfig();
            if (parc == 0)
                return null;
            String[] res = new String[parc];
            for (int i = 0; i < parc; i++)
                res[i] = pars[i].ToString();
            return res;
        }

        public void SetConfig(String param, String val)
        {
            //ReloadConfig();
            Boolean parExists = false;
            if (parc == 0)
            {
                pars.Add(new Parameter((param + ":" + val).ToString().ToLower()));
                //SaveConfig();     //save only after saving by user
                parc++;
                return;
            }

            for (int i = 0; i < pars.Count; i++)        //par exists; change it!
                if (pars[i].GetName().Equals(param))
                {
                    pars[i].SetValue(val.ToLower());
                    parExists = true;
                    break;
                }
            if (!parExists) pars.Add(new Parameter(param + ":" + val)); //par don't exists!
            //SaveConfig();
        }

        private Boolean ParameterExists(Parameter parameter)
        {
            if (parc == 0)
                return false;
            foreach (Parameter param in pars)
                if (param.GetName().Equals(parameter.GetName())) return true;
            return false;
        }

        public String GetConfig(String param)
        {
            //ReloadConfig();
            if (parc == 0) return "null";
            for (int i = 0; i < pars.Count; i++)
                if (pars[i].GetName().Equals(param))
                {
                    return pars[i].GetValue();
                }
            return "null";
        }

        public void SaveConfig()           //config from mem to fs
        {
            File.Create(path).Close();
            StreamWriter sw = File.AppendText(path);
            foreach (Parameter par in pars)
            {
                if(!(par.GetValue().Equals("null")&&par.GetName().Equals("null")))
                    sw.WriteLine(par.ToString().ToLower());
            }
            sw.Close();
        }
    }
}