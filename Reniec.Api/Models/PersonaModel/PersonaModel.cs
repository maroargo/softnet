using System;
using System.Collections.Generic;

namespace Reniec.Api.Models
{

    /// <summary>
    /// Clase DTO para gestionar lista de entidades
    /// </summary>
    public class PersonaItemsDto
    {
        public int IdPersona { get; set; }
        public int IdTipoDocumento { get; set; }
        public System.Guid? Guid { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string ApellidoCasada { get; set; }

        public int IdSexo { get; set; }
        public bool? SoloUnApellido { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdUbigeoNacimiento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdUbigeoDomicilio { get; set; }
        public int? IdNacionalidad { get; set; }

        public string RutaFoto { get; set; }
        public string NombreArchivoFoto { get; set; }
        //public TipoDocumento TipoDocumento { get; set; }

        public bool ValidadoRENIEC { get; set; }
        public string NumeroDocumento { get; set; }
        public bool EsEliminado { get; set; }

        public int IdTblEstadoValidacionPersona { get; set; }
        public DateTime? FechaUltimaValidacion { get; set; }

        // Agregado: 19/09/2019
        public string DescDocumento { get; set; }
        public string DescSexo { get; set; }
        public string DescPais { get; set; }

        public int? IdIngresante { get; set; }
        public int? IdDocente { get; set; }
        public int? IdAdministrativo { get; set; }
        public bool? ConAntecedente { get; set; }
    }

    public class PersonaDatosComplementariosItem
    {
        public int IdPersonaComplementario { get; set; }
        public int? IdPersona { get; set; }
        public System.Guid? Guid { get; set; }
        public string CodigoOrcid { get; set; }
        public bool? CondicionDiscapacidad { get; set; }
        public string CorreoPersonal { get; set; }
        public int? IdUbigeoResidencia { get; set; }
        public bool EsEliminado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public System.Guid UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public System.Guid? UsuarioModificacion { get; set; }
        public string IpModificacion { get; set; }
    }

    public class PersonaIdsRequest
    {
        public string IdsPersonas { get; set; }
    }

    public class PersonaDatoRequest
    {
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
    }

    public class PersonaDocumentoRequest
    {
        public int TipoDocumento { get; set; }
        public string Numerodocumento { get; set; }
    }

    public class PersonaTipoDocRequest
    {
        public int tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string IdsEntidad { get; set; }
        public int? IdNivelAcademico { get; set; }
        public int? IdPeriodoAcademico { get; set; }
    }

    public class PersonaMatriculadoRequest
    {
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int IdPeriodoAcademico { get; set; }
        public int IdModalidadEstudio { get; set; }
        public int IdEntidadUnidad { get; set; }
        public int IdEntidadPrograma { get; set; }
    }

    public class PersonaActualizacionDto
    {
        public int IdPersona { get; set; }
        public int IdTblTipoDocumentoIdentidad { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public bool? SoloUnApellido { get; set; }
        public int IdTblSexo { get; set; }
        public string FechaNacimiento { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public int? IdNacionalidad { get; set; }
        public int? IdUbigeoNacimiento { get; set; }
        public int? IdUbigeoDomicilio { get; set; }
        public string RutaFoto { get; set; }
        public string NombreArchivoFoto { get; set; }

        //additional data
        public int? IdPersonaDatoComplementario { get; set; }
        public string IdTblLenguaNativa { get; set; }
        public string IdTblIdiomaExtranjero { get; set; }
        public string CodigoOrcid { get; set; }
        public bool? CondicionDiscapacidad { get; set; }
        public string CorreoPersonal { get; set; }
        public int? IdUbigeoResidencia { get; set; }

        public string CodigoDepartamentoNacimiento { get; set; }
        public string CodigoProvinciaNacimiento { get; set; }
        public string CodigoDepartamentoResidencia { get; set; }
        public string CodigoProvinciaResidencia { get; set; }
        public string CorreoInstitucional { get; set; }
    }
    public class PersonaActualizar
    {
        public int IdPersona { get; set; }
        public int IdTblTipoDocumentoIdentidad { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        //public bool? SoloUnApellido { get; set; }
        public int IdTblSexo { get; set; }
        public int? IdNacionalidad { get; set; }
        public string CorreoPersonal { get; set; }
    }
    public class PersonaRequest
    {
        public PersonaDatoRequest personaDato { get; set; }
        public PersonaDocumentoRequest personaDocumento { get; set; }
        public bool? modoConsulta { get; set; }
    }

    public class PersonaItemCoreDto
    {
        public int IdPersona { get; set; }
        public int IdTipoDocumento { get; set; }
        public string DescDocumento { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public int IdSexo { get; set; }
        public string DescSexo { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public string DescPais { get; set; }
        public string NumeroDocumento { get; set; }
    }

    public class PersonaIdiomasItem
    {
        public int IdPersonaIdioma { get; set; }
        public int IdPersona { get; set; }
        public int? IdTblIdiomaExtranjero { get; set; }
        public System.Guid? Guid { get; set; }
        public bool EsEliminado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public System.Guid UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public System.Guid? UsuarioModificacion { get; set; }
        public string IpModificacion { get; set; }
    }

    public class PersonaLenguasNativasItem
    {
        public int IdPersonaLenguaNativa { get; set; }
        public int IdPersona { get; set; }
        public int? IdTblLenguaNativa { get; set; }
        public System.Guid? Guid { get; set; }
        public bool EsEliminado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public System.Guid UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public System.Guid? UsuarioModificacion { get; set; }
        public string IpModificacion { get; set; }
    }


    #region VALIDACION RENIEC

    public class PersonaValidacionBusquedaRequest
    {
        public string IdTipoPersona { get; set; }
        public string IdEstadoValidacionPersona { get; set; }
        public string IdsPersonas { get; set; }
        public string CodigoEntidad { get; set; }
        public string DetalleObservacion { get; set; }
    }

    public class PersonaValidacionPageItemResponse
    {
        public long? RowNum { get; set; }
        public int? TotalFilas { get; set; }
        public int? IdPersona { get; set; }
        public int? IdTblTipoDocumentoIdentidad { get; set; }
        public int? IdTblEstadoValidacionPersona { get; set; }
        public int? IdTblTipoPersona { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string ApellidoCasada { get; set; }
        public int? IdTblSexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdEntidad { get; set; }
        public string CodigoEntidad { get; set; }
        public string DetalleObservacion { get; set; }
        public int? IdPersonaTipo { get; set; }
        public DateTime? FechaUltimaValidacion { get; set; }
        public string TipoPersona { get; set; }
        public string Entidad { get; set; }
        public string Sexo { get; set; }
        public string EstadoValidacion { get; set; }
        public bool LlavePrincipal { get; set; }
        public int? IdUbigeoNacimiento { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string FechaNacimientoExportar { get; set; }
        public string FechaUltimaValidacionExportar { get; set; }
    }

    public class PersonaValidacionResponse
    {
        public int? IdPersona { get; set; }
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
        public string CodigoPaisNacimiento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public IList<string> DetalleObservacion { get; set; }
        public bool LlavePrincipal { get; set; }
        public string PaisNacimiento { get; set; }
        public string UbigeoNacimiento { get; set; }
        public DateTime? FechaUltimaValidacion { get; set; }
        public string EstadoValidacion { get; set; }
        public string Sexo { get; set; }
    }
    #endregion

}
