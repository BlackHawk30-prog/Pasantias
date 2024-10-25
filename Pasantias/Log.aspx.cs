using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Pasantias
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Modelo.Log slog = new Modelo.Log(@"C:\Users\TECH EXPRESS\Desktop\Pasantias");

            slog.Add("Hola mundo");

        }
    }
}