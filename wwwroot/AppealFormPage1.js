var currentLanguage = "en";

var translations = {
    en: {
        title: "SAP Appeal Form",
        "toggle-button": "Español",
        "student-info-heading": "Student Information",
        "label-name": "Name:",
        "label-student-id": "Student ID#:",
        "label-date": "Date:",
        "email-name": "Email:",
        "label-major": "Major:",
        "appeal-details-heading": "Appeal Explanation",
        "appeal-details-text": "Please provide a signed, typed, or written statement (max 500 words) explaining:",
        "appeal-list-1": "Any extenuating circumstances that led to your SAP status.",
        "appeal-list-2": "How these circumstances affected your academic progress.",
        "appeal-list-3": "Steps you have taken to overcome them.",
        "appeal-list-4": "Your plan for future academic success.",
        "acknowledgements-heading": "Acknowledgements",
        "checkbox1-text": "Payment arrangement: I understand the Office of Financial Aid will NOT hold my classes pending a decision by the SAP committee if I am unable to pay any balance on the account.",
        "checkbox2-text": "I understand that the decision of the Office of Financial Aid is final.",
        "submit-button": "Submit Appeal"
    },
    es: {
        title: "Formulario de Apelación SAP",
        "toggle-button": "English",
        "student-info-heading": "Información del Estudiante",
        "label-name": "Nombre:",
        "label-student-id": "ID del Estudiante:",
        "label-date": "Fecha:",
        "email-name": "Correo Electrónico:",
        "label-major": "Especialidad:",
        "appeal-details-heading": "Explicación de la Apelación",
        "appeal-details-text": "Por favor, proporcione una declaración firmada, mecanografiada o escrita (máximo 500 palabras) explicando:",
        "appeal-list-1": "Cualquier circunstancia atenuante que llevó a su estado SAP.",
        "appeal-list-2": "Cómo estas circunstancias afectaron su progreso académico.",
        "appeal-list-3": "Los pasos que ha tomado para superarlos.",
        "appeal-list-4": "Su plan para el éxito académico futuro.",
        "acknowledgements-heading": "Reconocimientos",
        "checkbox1-text": "Arreglo de pago: Entiendo que la Oficina de Ayuda Financiera NO retendrá mis clases a la espera de una decisión del comité SAP si no puedo pagar el saldo de la cuenta.",
        "checkbox2-text": "Entiendo que la decisión de la Oficina de Ayuda Financiera es definitiva.",
        "submit-button": "Enviar Apelación"
    }
};

function toggleLanguage() {
    currentLanguage = currentLanguage === "en" ? "es" : "en";

    // Update the toggle button text
    document.getElementById("toggle-button").innerText = translations[currentLanguage]["toggle-button"];

    // Loop through all translations
    Object.keys(translations[currentLanguage]).forEach(function (key) {
        var element = document.getElementById(key);

        if (element) {
            // Check if it's a label with a span (for form inputs)
            if (element.querySelector(".label-text")) {
                element.querySelector(".label-text").innerText = translations[currentLanguage][key];
            }
            // Otherwise, update normally
            else {
                element.innerText = translations[currentLanguage][key];
            }
        }
    });
}

function wordLimit() {
    var textArea = document.getElementById('text-area');
    var words = textArea.value.split(/\s+/).filter(function(word) { return word.length > 0; });
    var maxWords = 500;

    if (words.length > maxWords) {
        var truncatedText = words.slice(0, maxWords).join(' ');
        textArea.value = truncatedText;
        alert("You've reached the maximum word limit of 500 words.");
    }

    checkFormValidity();
}

function checkFormValidity() {
    var checkbox1 = document.getElementById('checkbox1');
    var checkbox2 = document.getElementById('checkbox2');
    var submitButton = document.getElementById('submit-button');
    var textArea = document.getElementById('text-area');

    // Enable the submit button only if both checkboxes are checked and text area has content
    if (checkbox1 && checkbox2 && textArea &&
        checkbox1.checked && checkbox2.checked && textArea.value.trim() !== '') {
        submitButton.disabled = false;
    } else {
        submitButton.disabled = true;
    }
}

// Add event listener to text area
document.addEventListener('DOMContentLoaded', function() {
    var textArea = document.getElementById('text-area');
    if (textArea) {
        textArea.addEventListener('input', checkFormValidity);
    }
});