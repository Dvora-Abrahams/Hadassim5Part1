const baseUrl = "/Suppliers";

async function supplierLogin() {
    const companyName = document.getElementById("companyNameLogin").value;
    const phoneNumber = document.getElementById("phoneNumberLogin").value;

    if (!companyName || !phoneNumber) {
        alert("נא למלא את כל השדות");
        return;
    }

    const response = await fetch(`${baseUrl}/RegisteredSupplier?company=${companyName}&phone=${phoneNumber}`, {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        }
    });

    const isLoggedIn = await response.json(); // מצפים לקבל ערך בוליאני (true או false)

    if (isLoggedIn) {
        // שמירת שם החברה ל-localStorage כדי שנוכל להשתמש בו בפונקציות אחרות
        localStorage.setItem("supplierCompany", companyName);

        // מעבר לדף הספק
        window.location.href = "supplier.html";
    } else {
        alert("הכניסה נכשלה, אנא בדוק את הפרטים ונסה שוב");
    }
}


async function supplierRegister() {
    const companyName = document.getElementById("companyName").value;
    const phoneNumber = document.getElementById("phoneNumber").value;
    const contactName = document.getElementById("contactName").value;

    if (!companyName || !phoneNumber || !contactName) {
        alert("נא למלא את כל השדות");
        return;
    }

    const response = await fetch(`${baseUrl}/creatSupplier?company=${companyName}&phone=${phoneNumber}&contactName=${contactName}`, {
        method: 'POST',
        headers: {
            "Content-Type": "application/json"
        }
    });

    if (response.ok) {
        alert("הרשמה בוצעה בהצלחה, כעת תוכל להיכנס.");
    } else {
        alert("שגיאה בהרשמה, אנא נסה שוב");
    }
}

function toggleForm() {
    const registerForm = document.getElementById("registerForm");
    const loginForm = document.getElementById("loginForm");

    if (registerForm.style.display === "none") {
        registerForm.style.display = "block";
        loginForm.style.display = "none";
    } else {
        registerForm.style.display = "none";
        loginForm.style.display = "block";
    }
}
