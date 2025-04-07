const baseUrl = "/Suppliers";

document.addEventListener("DOMContentLoaded", () => {
    const section = document.querySelector("section");

    // שדה כמות כללית
    const quantityInput = document.createElement("input");
    quantityInput.type = "number";
    quantityInput.placeholder = "כמות כללית";
    quantityInput.id = "totalQuantity";
    section.appendChild(quantityInput);


    const addBtn = document.createElement("button");
    addBtn.textContent = "הוסף שורה למוצר";
    addBtn.onclick = addProduct;
    section.appendChild(addBtn);

    const submitBtn = document.createElement("button");
    submitBtn.textContent = "שלח הזמנה";
    submitBtn.onclick = submitProducts ;
    section.appendChild(submitBtn);
});

function addProduct() {
    const container = document.getElementById("goodsContainer");

    const productDiv = document.createElement("div");
    productDiv.classList.add("product-line");

    const nameInput = document.createElement("input");
    nameInput.placeholder = "שם מוצר";
    nameInput.classList.add("product-name");

    const priceInput = document.createElement("input");
    priceInput.type = "number";
    priceInput.placeholder = "מחיר";
    priceInput.classList.add("product-price");

    const removeButton = document.createElement("button");
    removeButton.innerText = "🗑️";
    removeButton.onclick = () => productDiv.remove();

    productDiv.appendChild(nameInput);
    productDiv.appendChild(priceInput);
    productDiv.appendChild(removeButton);

    container.appendChild(productDiv);
}

async function submitProducts() {
    const goods = {};
    const productNames = document.querySelectorAll(".product-name");
    const productPrices = document.querySelectorAll(".product-price");
    const quantity = document.getElementById("totalQuantity").value;

    if (isNaN(quantity) || quantity <= 0) {
        alert("נא להזין כמות תקינה.");
        return;
    }

    for (let i = 0; i < productNames.length; i++) {
        const name = productNames[i].value.trim();
        const price = parseFloat(productPrices[i].value);

        if (!name || isNaN(price) || price <= 0) {
            alert("נא להזין שם ומחיר תקינים לכל מוצר");
            return;
        }

        goods[name] = price;
             
        
    }

    // הצגת "LOADING..."
    document.getElementById("loading").style.display = "block";

    const company = localStorage.getItem("supplierCompany");
    if (!company) {
        alert("לא ניתן לאתר את שם החברה. אנא התחבר שוב.");
        return;
    }

    const response = await fetch(`${baseUrl}/AddGoodsToSupplier?company=${encodeURIComponent(company)}&n=${quantity}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(goods)
    });

    // הסתרת "LOADING..."
    document.getElementById("loading").style.display = "none";

    if (response.ok) {
        alert("המוצרים נוספו בהצלחה!");
    } else {
        alert("אירעה שגיאה בהוספת המוצרים");
    }
}


async function getOrders() {
   
    const company = localStorage.getItem("supplierCompany");
    if (!company) {
        alert("לא ניתן לאתר את שם החברה. אנא התחבר שוב.");
        return;
    }

    const response = await fetch(`${baseUrl}/GetOrderByCompany?company=${encodeURIComponent(company)}`);
    if (!response.ok) {
        alert("שגיאה בקבלת ההזמנות מהשרת.");
        return;
    }
   
    const orders = await response.json();

    const ordersList = document.getElementById("ordersList");
    if (ordersList.innerHTML !== "") {
        ordersList.innerHTML = "";
        return;
    }
    ordersList.innerHTML = "";

    if (!orders || orders.length === 0) {
        ordersList.innerHTML = "<p>אין הזמנות להצגה.</p>";
        return;
    }

    orders.forEach(order => {
        const orderDiv = document.createElement("div");
        orderDiv.className = "order";

        const goodsHtml = order.goods.length > 0
            ? `<ul>${order.goods.map(g => `<li>${g.productName} - ₪${g.price.toFixed(2)}</li>`).join("")}</ul>`
            : "<p>אין מוצרים בהזמנה זו.</p>";

        orderDiv.innerHTML = `
            <h3>הזמנה #${order.id}</h3>
            <p><strong>סטטוס:</strong> ${order.status}</p>
            <p><strong>ספק:</strong> ${order.supplierId ?? "לא ידוע"}</p>
            <p><strong>מחיר סופי::</strong> ${order.finalPrice}</p>
            <p><strong>מוצרים:</strong></p>
            ${goodsHtml}
        `;

        ordersList.appendChild(orderDiv);
    });
}

async function getCompletedOrder() {
    const company = localStorage.getItem("supplierCompany");
    if (!company) {
        alert("לא ניתן לאתר את שם החברה. אנא התחבר שוב.");
        return;
    }

    const response = await fetch(`${baseUrl}/GetCompletedOrderByCompany?company=${encodeURIComponent(company)}`);
    if (!response.ok) {
        alert("שגיאה בקבלת ההזמנות מהשרת.");
        return;
    }

    const orders = await response.json();

    const ordersList = document.getElementById("completedOrdersList");
    if (ordersList.innerHTML !== "") {
        ordersList.innerHTML = "";
        return;
    }
    ordersList.innerHTML = "";

    if (!orders || orders.length === 0) {
        ordersList.innerHTML = "<p>אין הזמנות להצגה.</p>";
        return;
    }

    orders.forEach(order => {
        const orderDiv = document.createElement("div");
        orderDiv.className = "order";

        const goodsHtml = order.goods.length > 0
            ? `<ul>${order.goods.map(g => `<li>${g.productName} - ₪${g.price.toFixed(2)}</li>`).join("")} }</ul>`
            : "<p>אין מוצרים בהזמנה זו.</p>";

        orderDiv.innerHTML = `
            <h3>הזמנה #${order.id}</h3>
            <p><strong>הודעה מבעל המכולת: </strong> הזמנה מספר ${order.id} הושלמה בהצלחה</p>
            <p><strong>סטטוס:</strong> ${order.status}</p>
            <p><strong>מחיר סופי::</strong> ${order.finalPrice}</p>
            <p><strong>מוצרים:</strong></p>
            ${goodsHtml}
        `;

        ordersList.appendChild(orderDiv);
    });
}



async function confirmReceipt() {
    const orderId = document.getElementById("orderIdInput").value;

    if (!orderId || isNaN(orderId) || orderId <= 0) {
        alert("נא להזין מזהה הזמנה תקין.");
        return;
    }

    const company = localStorage.getItem("supplierCompany");
    if (!company) {
        alert("לא ניתן לאתר את שם החברה. אנא התחבר שוב.");
        return;
    }

    document.getElementById("loading").style.display = "block";

    try {
        const response = await fetch(`${baseUrl}/ConfirmationReceipOrder?orderId=${orderId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            }
        });

        document.getElementById("loading").style.display = "none";

        if (!response.ok) {
            alert("אירעה שגיאה בתקשורת עם השרת.");
            return;
        }

        const result = await response.json(); // כאן מתקבל ה-boolean

        if (result === true) {
            alert("ההזמנה אושרה בהצלחה!");
        } else {
            alert("האישור נכשל. אולי ההזמנה כבר אושרה?");
        }

    } catch (error) {
        document.getElementById("loading").style.display = "none";
        alert("שגיאה בעת שליחת הבקשה.");
    }
}



