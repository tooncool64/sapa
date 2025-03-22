var currentLanguage = "en";

var translations = {
    en: {
        "toggle-button": "Español",
        "page-title": "Reasons & Supporting Documentation for SAP Appeal",
        "reason-1-title": "1. Personal Illness or Injury (Physical and Mental Health)",
        "reason-1a": "a. A detailed explanation of the medical circumstances including the date.",
        "reason-1b": "b. Supporting documentation.",
        "reason-1c": "c. Documentation regarding what steps you've taken.",

        "reason-2-title": "2. Illness of a Family Member",
        "reason-2a": "a. A detailed explanation of the medical circumstances of the family member.",
        "reason-2b": "b. Supporting documentation.",
        "reason-2c": "c. Documentation regarding what steps you've taken.",

        "reason-3-title": "Death of a Family Member/ Roommate/ Close Friend",
        "reason-3a": "a. A detailed statement including the name of the deceased and their relationship.",
        "reason-3b": "b. Supporting documentation.",
        "reason-3c": "c. Documentation demonstrating your ability to return to classes.",

        "reason-4-title": "Personal Crisis",
        "reason-4a": "a. A detailed explanation of the crisis including the date of occurrence, duration, and impact on coursework.",
        "reason-4b": "b. Supporting documentation.",
        "reason-4c": "c. Documentation regarding what steps you've taken to resolve the crisis.",

        "reason-5-title": "Other Circumstances Beyond Your Control",
        "reason-5a": "a. Provide documentation of extenuating circumstances beyond your control.",
        "reason-5b": "b. Documentation supporting that your circumstances have either been resolved or are being managed.",

        "reason-6-title": "Exceeded Maximum Hours for Degree Completion",
        "reason-6a": "a. A detailed explanation for exceeding the maximum hours required.",
        "reason-6b": "b. A prescribed academic plan of work from an academic advisor.",

        "documentation-label": "Official documentation supporting your extenuating circumstance(s):",
        "documentation-description": "The only acceptable forms of documentation are PDF and Word.",

        "final-note": "Note: Appeals submitted for reasons above will be reviewed on a case-by-case basis. Appeals are not automatically approved for any of the above reasons but are reviewed based on circumstances, documented academic history, and academic success potential."
    },

    es: {
        "toggle-button": "English",
        "page-title": "Razones y Documentación de Apoyo para la Apelación SAP",
        "reason-1-title": "1. Enfermedad o Lesión Personal (Salud Física y Mental)",
        "reason-1a": "a. Una explicación detallada de las circunstancias médicas, incluida la fecha.",
        "reason-1b": "b. Documentación de respaldo.",
        "reason-1c": "c. Documentación sobre los pasos que ha tomado.",

        "reason-2-title": "2. Enfermedad de un Familiar",
        "reason-2a": "a. Una explicación detallada de las circunstancias médicas del familiar.",
        "reason-2b": "b. Documentación de respaldo.",
        "reason-2c": "c. Documentación sobre los pasos que ha tomado.",

        "reason-3-title": "Fallecimiento de un Familiar/Compañero de Cuarto/Amigo Cercano",
        "reason-3a": "a. Una declaración detallada que incluya el nombre del fallecido y su relación.",
        "reason-3b": "b. Documentación de respaldo.",
        "reason-3c": "c. Documentación que demuestre su capacidad para regresar a clases.",

        "reason-4-title": "Crisis Personal",
        "reason-4a": "a. Una explicación detallada de la crisis, incluida la fecha de ocurrencia, duración e impacto en el curso.",
        "reason-4b": "b. Documentación de respaldo.",
        "reason-4c": "c. Documentación sobre los pasos que ha tomado para resolver la crisis.",

        "reason-5-title": "Otras Circunstancias Fuera de su Control",
        "reason-5a": "a. Proporcione documentación de circunstancias atenuantes fuera de su control.",
        "reason-5b": "b. Documentación que respalde que sus circunstancias han sido resueltas o están siendo manejadas.",

        "reason-6-title": "Excedió el Máximo de Horas para la Finalización del Grado",
        "reason-6a": "a. Una explicación detallada de por qué excedió el número máximo de horas requeridas.",
        "reason-6b": "b. Un plan académico prescrito por un asesor académico.",

        "documentation-label": "Documentación oficial que respalde sus circunstancias atenuantes:",
        "documentation-description": "Las únicas formas aceptables de documentación son PDF y Word.",

        "final-note": "Nota: Las apelaciones enviadas por las razones anteriores se revisarán caso por caso. Las apelaciones no se aprueban automáticamente por ninguna de las razones anteriores, sino que se revisan en función de las circunstancias, el historial académico documentado y el potencial de éxito académico."
    }
};

function toggleLanguage() {
    currentLanguage = currentLanguage === "en" ? "es" : "en";
    document.getElementById("toggle-button").innerText = translations[currentLanguage]["toggle-button"];

    Object.keys(translations[currentLanguage]).forEach(function (key) {
        var element = document.getElementById(key);
        if (element) {
            element.innerText = translations[currentLanguage][key];
        }
    });
}