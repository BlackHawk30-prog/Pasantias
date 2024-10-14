using Microsoft.Ajax.Utilities;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pasantias
{
    public partial class Hoja_Tiempo : System.Web.UI.Page
    {
       HojaTiempo hojaTiempo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hojaTiempo = new HojaTiempo();
                hojaTiempo.grid_hojas(grid_hojas);
                
            }
        }

        protected void Btn_Agregar_Click(object sender, EventArgs e)
        {

        }
    }
}