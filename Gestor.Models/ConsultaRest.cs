namespace Gestor.UI.Models.Dtos
{
    public class ConsultaRest
    {
       
        
            public string? URL { get; set; }
            public object Cuerpo { get; set; } = null;
            public MetodoREST MetodoRest { get; set; } = MetodoREST.GET;
            public TipoDeContenido TipoDeContenido { get; set; } = TipoDeContenido.Json;


     }

    public class RespuestaAPi
    {

        public bool esSucces { get; set; } = true;
        public object? Respuesta { get; set; }
        public string? Mensaje { get; set; }



    }


    public enum MetodoREST
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    public enum TipoDeContenido
    {
        Json,
        Null
    }




}
