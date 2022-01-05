using System;

namespace Reniec.Api.Models
{
    public class PersonaReniecResponseModel
    {
        public string codigoError { get; set; }
        public string numeroDNI { get; set; }
        public string digitoVerificacion { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string apellidoCasada { get; set; }
        public string nombres { get; set; }
        public string codigoUbigeoContinenteDomicilio { get; set; }
        public string codigoUbigeoPaisDomicilio { get; set; }
        public string codigoUbigeoDepartamentoDomicilio { get; set; }
        public string codigoUbigeoProvinciaDomicilio { get; set; }
        public string codigoUbigeoDistritoDomicilio { get; set; }
        public string continenteDomicilio { get; set; }
        public string paisDomicilio { get; set; }
        public string departamentoDomicilio { get; set; }
        public string provinciaDomicilio { get; set; }
        public string distritoDomicilio { get; set; }
        public string estadoCivil { get; set; }
        public string codigoGradoInstruccion { get; set; }
        public string estatura { get; set; }
        public string sexo { get; set; }
        public string tipoDocumentoSustentatorio { get; set; }
        public string numeroDocumentoSustentatorio { get; set; }
        public string codigoUbigeoContinenteNacimiento { get; set; }
        public string codigoUbigeoPaisNacimiento { get; set; }
        public string codigoUbigeoDepartamentoNacimiento { get; set; }
        public string codigoUbigeoProvinciaNacimiento { get; set; }
        public string codigoUbigeoDistritoNacimiento { get; set; }
        public string continenteNacimiento { get; set; }
        public string paisNacimiento { get; set; }
        public string departamentoNacimiento { get; set; }
        public string provinciaNacimiento { get; set; }
        public string distritoNacimiento { get; set; }
        public string fechaNacimiento { get; set; }
        public string nombrePadre { get; set; }
        public string nombreMadre { get; set; }
        public string fechaInscripcion { get; set; }
        public string fechaExpediente { get; set; }
        public string constanciaVotacion { get; set; }
        public string fechaCaducidad { get; set; }
        public string restriccion { get; set; }
        public string prefijoDireccion { get; set; }
        public string direccion { get; set; }
        public string numeroDireccion { get; set; }
        public string blockChalet { get; set; }
        public string interior { get; set; }
        public string urbanizacion { get; set; }
        public string etapa { get; set; }
        public string manzana { get; set; }
        public string lote { get; set; }
        public string prefijoBlockChalet { get; set; }
        public string prefijoDepartamentoPiso { get; set; }
        public string prefijoUrbCond { get; set; }
        public string reservado { get; set; }
        public string descripcionError { get; set; }
        public string foto { get; set; }
        public string firma { get; set; }
        public string tramaEnvio { get; set; }
        public string tramaRespuesta { get; set; }

        public string codigoUbigeoNacimiento
        {
            get
            {
                return $"{codigoUbigeoDepartamentoNacimiento}{codigoUbigeoProvinciaNacimiento}{codigoUbigeoDistritoNacimiento}";
            }
        }

        public bool soloUnApellido
        {
            get
            {
                return (string.IsNullOrEmpty(apellidoPaterno) && !string.IsNullOrEmpty(apellidoMaterno)) ||
                       (!string.IsNullOrEmpty(apellidoPaterno) && string.IsNullOrEmpty(apellidoMaterno));
            }
        }

        public DateTime? FechaNacimientoDate
        {
            get
            {
                if (string.IsNullOrEmpty(fechaNacimiento)) return null;

                var anio = Convert.ToInt32(fechaNacimiento.Substring(0, 4));
                var mes = Convert.ToInt32(fechaNacimiento.Substring(4, 2));
                var dia = Convert.ToInt32(fechaNacimiento.Substring(6, 2));

                return new DateTime(anio, mes, dia);
            }
        }

        public (PersonaValidacionResponse persona, string msg) MapToPersonaValidacion()
        {
            switch (codigoError)
            {
                case "0000":
                    return (new PersonaValidacionResponse()
                    {
                        Nombres = nombres,
                        PrimerApellido = apellidoPaterno,
                        SegundoApellido = apellidoMaterno,
                        ApellidoCasada = apellidoCasada,
                        NumeroDocumento = numeroDNI,
                        IdSexo = Convert.ToInt32(sexo),
                        SoloUnApellido = soloUnApellido,
                        FechaNacimiento = FechaNacimientoDate,
                        CodigoPaisNacimiento = $"{codigoUbigeoContinenteNacimiento}{codigoUbigeoPaisNacimiento}",
                        PaisNacimiento = paisNacimiento,
                        UbigeoNacimiento = $"{departamentoNacimiento}-{provinciaNacimiento}-{distritoNacimiento}",
                        Sexo = (sexo == "1" ? "Masculino" : "Femenino")
                    }, null);

                case "5001":
                    return (null, "El DNI registrado no existe en RENIEC");

                default: return (null, descripcionError);
            }
        }
    }

    public class UpdatePersonaReniecRequest
    {
        public string IdEntidad { get; set; }
        public string CodigoEntidad { get; set; }
        public int? IdPersonaAntiguo { get; set; }
        public int? IdPersonaNuevo { get; set; }
    }
}
