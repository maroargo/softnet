using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Reniec.Api.Models
{
    public class PersonaValidacionRequest
    {

        public int? IdPersona { get; set; }
        public int? IdTipoPersona { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? IdEstadoValidacion { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string ApellidoCasada { get; set; }
        public string NumeroDocumento { get; set; }
        public int? IdSexo { get; set; }
        public bool? SoloUnApellido { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdUbigeoNacimiento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public bool? CambioDni { get; set; }
        public bool AsociadoMasDeUnRegistro { get; set; }
        public PersonaReniecResponseModel personaReniec { get; set; }
        public string CodigoPaisNacimiento { get; set; }
        public string UbigeoNacimiento { get; set; }
        public bool? LlavePrincipal { get; set; }
    }

    [DataContract]
    public class CorregirSaveRequest
    {
        [DataMember]
        public List<string> documentosCorregir { get; set; }
    }
}
