var currentLanguage = "en";

var translations = {
    en: {
        "toggle-button": "Español",
        "sap-title": "Satisfactory Academic Progress (SAP) Appeal Portal",
        "sap-intro1": "If you are a student seeking information on maintaining eligibility or need to submit an appeal, please log in using your Saint Martin's credentials.",
        "sap-intro2": "If you are an advisor here to approve a student’s appeal, or an administrator reviewing SAP appeals, log in with your Saint Martin's credentials as well.",
        "eligibility-title": "Maintaining Eligibility",
        "eligibility-text": "In order to maintain your eligibility for financial aid, you must demonstrate academic progress towards a degree program. We utilize both qualitative and quantitative measures to determine Satisfactory Academic Progress (SAP). Please note that SAP is a process completed strictly to determine your financial aid eligibility and NOT your academic eligibility.",
        "sap-status-title": "SAP Status",
        "warning-title": "Warning",
        "warning-text": "Students who do not meet the GPA or pace of completion requirements for the first time will be placed in a warning status. If you are in warning status, you will be eligible to receive available financial aid for one semester. After your warning semester, disbursements may be delayed while your satisfactory academic progress is reviewed.",
        "must-appeal-title": "Must Appeal",
        "must-appeal-text": "Students who do not meet the GPA or pace of completion requirements following a warning status semester are not eligible to receive federal, state, or institutional aid and are placed on a must appeal status. In addition, students for whom it is mathematically impossible to complete their degree within the maximum timeframe provided above will be placed on a must appeal status immediately.",
        "must-appeal-extra": "Some alternative loan and scholarship programs require students to be in good standing under SAP guidelines. You may submit a completed appeal to the Office of Financial Aid which documents extenuating circumstances that interfered with your academic performance.",
        "appeal-process-title": "Appeal Process",
        "appeal-text": "Students on 'must appeal' status have the opportunity to appeal their aid eligibility. Appeals must be submitted by mid-semester. Appeals submitted after the deadline will be reviewed but late petitions may result in the loss of funding for the term. Appeals must include a written statement, academic plan, and documentation of extenuating circumstances.",
        "approved-appeals-title": "Approved Appeals",
        "approved-appeals-text": "Students will be notified of an approved appeal and placed in probation status with their aid reinstated for one semester. Students may be asked to submit a new academic plan for each semester of probation status until they reach good standing. If the student does not pass the credits or earn the GPA given in an academic plan, they will be placed in must appeal status again.",
        "denied-appeals-title": "Denied Appeals",
        "denied-appeals-text": "If an appeal is denied, aid will be removed from the following semester, and the student is required to find other financial options to apply towards the student balance.",
        "denied-extra": "If the original appeal did not include information that may have been helpful for the Office of Financial Aid to know when evaluating the appeal, the student may submit additional information to provide clarity to be reviewed again.",
        "final-note": "All decisions by the Office of Financial Aid are final."
    },
    es: {
        "toggle-button": "English",
        "sap-title": "Portal de Apelación de Progreso Académico Satisfactorio (SAP)",
        "sap-intro1": "Si eres un estudiante que busca información sobre cómo mantener la elegibilidad o necesitas presentar una apelación, inicia sesión con tus credenciales de Saint Martin.",
        "sap-intro2": "Si eres un asesor aquí para aprobar la apelación de un estudiante o un administrador revisando apelaciones SAP, inicia sesión con tus credenciales de Saint Martin.",
        "eligibility-title": "Mantenimiento de la Elegibilidad",
        "eligibility-text": "Para mantener tu elegibilidad para ayuda financiera, debes demostrar progreso académico hacia un programa de grado. Utilizamos tanto medidas cualitativas como cuantitativas para determinar el Progreso Académico Satisfactorio (SAP). Ten en cuenta que SAP es un proceso completado estrictamente para determinar tu elegibilidad para ayuda financiera y NO tu elegibilidad académica.",
        "sap-status-title": "Estado SAP",
        "warning-title": "Advertencia",
        "warning-text": "Los estudiantes que no cumplan con los requisitos de GPA o ritmo de finalización serán colocados en estado de advertencia. Si estás en estado de advertencia, serás elegible para recibir la ayuda financiera disponible durante un semestre. Después de tu semestre de advertencia, los desembolsos pueden retrasarse mientras se revisa tu progreso académico satisfactorio.",
        "must-appeal-title": "Debe Apelar",
        "must-appeal-text": "Los estudiantes que no cumplan con los requisitos de GPA o ritmo de finalización después de un semestre de advertencia no son elegibles para recibir ayuda federal, estatal o institucional y son colocados en estado de debe apelar. Además, los estudiantes para quienes sea matemáticamente imposible completar su grado dentro del plazo máximo proporcionado serán colocados en estado de debe apelar inmediatamente.",
        "must-appeal-extra": "Algunos programas de préstamos alternativos y becas requieren que los estudiantes estén en buen estado bajo las pautas de SAP. Puede enviar una apelación completa a la Oficina de Ayuda Financiera que documente las circunstancias atenuantes que afectaron su rendimiento académico.",
        "appeal-process-title": "Proceso de Apelación",
        "appeal-text": "Los estudiantes en estado de 'deben apelar' tienen la oportunidad de apelar su elegibilidad para recibir ayuda financiera. Las apelaciones deben presentarse antes de la mitad del semestre. Las apelaciones presentadas después de la fecha límite serán revisadas, pero las solicitudes tardías pueden resultar en la pérdida de fondos para el período académico. Las apelaciones deben incluir una declaración escrita, un plan académico y documentación de circunstancias atenuantes.",
        "approved-appeals-title": "Apelaciones Aprobadas",
        "approved-appeals-text": "Los estudiantes serán notificados de una apelación aprobada y serán colocados en estado de prueba con su ayuda restablecida por un semestre. Es posible que se les pida a los estudiantes que presenten un nuevo plan académico para cada semestre de estado de prueba hasta que alcancen el buen estado académico. Si el estudiante no aprueba los créditos o no obtiene el GPA requerido en un plan académico, será colocado nuevamente en estado de 'debe apelar'.",
        "denied-appeals-title": "Apelaciones Denegadas",
        "denied-appeals-text": "Si se deniega una apelación, la ayuda será eliminada del semestre siguiente y el estudiante deberá encontrar otras opciones financieras para pagar el saldo de su cuenta estudiantil.",
        "denied-extra": "Si la apelación original no incluyó información que podría haber sido útil para la Oficina de Ayuda Financiera al evaluar la apelación, el estudiante puede enviar información adicional para que sea revisada nuevamente.",
        "final-note": "Todas las decisiones de la Oficina de Ayuda Financiera son finales."
    }
};


function toggleLanguage() {
    currentLanguage = currentLanguage === "en" ? "es" : "en";
    document.getElementById("toggle-button").innerText = translations[currentLanguage]["toggle-button"];

    for (var key in translations[currentLanguage]) {
        if (document.getElementById(key)) {
            document.getElementById(key).innerText = translations[currentLanguage][key];
        }
    }
}