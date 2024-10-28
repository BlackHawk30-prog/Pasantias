using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Finanzas : System.Web.UI.Page
    {
        Finanzas finanzas;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                finanzas = new Finanzas();
                finanzas.grid_fina(grid_fina);
            }
        }
    }
}