using System;
using System.Collections.Generic;
using System.Reflection;

namespace DCC.SalesApp.Helpers
{
    public static class propertyCopy
    {
        public static void CopyProperties(this object source, object destination)
        {
            // Iterate the Properties of the destination instance and  
            // populate them from their source counterparts  
            int x = 1;
            IEnumerable <PropertyInfo> destinationProperties = destination.GetType().GetRuntimeProperties();
            foreach (PropertyInfo destinationPi in destinationProperties)
            {
                try
                {
                    if (destinationPi.Name == "ID" || destinationPi.Name == "SignatureImg")
                        continue;                    
                    PropertyInfo sourcePi = source.GetType().GetRuntimeProperty(destinationPi.Name);
                    destinationPi.SetValue(destination, sourcePi.GetValue(source, null), null);
                    x++;
                }
                catch(Exception exp)
                {
                    System.Diagnostics.Debug.WriteLine(exp.Message);
                    break;
                }
                

            }
        }
    }
}
