using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Hojas_Generales : System.Web.UI.Page
    {
        HojasGenerales Hojas;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Hojas = new HojasGenerales();
                Hojas.grid_Generales(grid_Generales);
            }

        }
    }
}