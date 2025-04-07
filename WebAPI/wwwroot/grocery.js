const baseUrl = "/Grocery";

document.addEventListener("DOMContentLoaded", () => {
    const addBtn = document.createElement("button");
    addBtn.textContent = "הוסף שורה להזמנה";
    addBtn.onclick = addProduct;
    document.querySelector("section").appendChild(addBtn);
    //document.getElementById("addProductBtn").addEventListener("click", addProduct);
    //document.getElementById("sendOrderBtn").addEventListener("click", sendOrder);

});

function addProduct() {
    const container = document.getElementById("productsContainer");

    const productDiv = document.createElement("div");
    productDiv.classList.add("product-line");

    const nameInput = document.createElement("input");
    nameInput.placeholder = "שם מוצר";
    nameInput.classList.add("product-name");

    const quantityInput = document.createElement("input");
    quantityInput.type = "number";
    quantityInput.placeholder = "כמות";
    quantityInput.classList.add("product-quantity");

    const removeButton = document.createElement("button");
    removeButton.innerText = "🗑️";
    removeButton.onclick = () => productDiv.remove();

    productDiv.appendChild(nameInput);
    productDiv.appendChild(quantityInput);
    productDiv.appendChild(removeButton);

    container.appendChild(productDiv);
}

function sendOrder() {
    const company = document.getElementById("company").value;
    if (!company) {
        alert("נא להזין שם חברה");
        return;
    }

    const productNames = document.querySelectorAll(".product-name");
    const productQuantities = document.querySelectorAll(".product-quantity");

    const products = [];

    for (let i = 0; i < productNames.length; i++) {
        const name = productNames[i].value.trim();
        const quantity = parseInt(productQuantities[i].value);

        if (!name || isNaN(quantity) || quantity <= 0) {
            alert("נא להזין שם וכמות תקינה לכל מוצר");
            return;
        }

        products.push({ name, quantity });
    }

    const queryParams = new URLSearchParams();
    queryParams.append("company", company);

    // הוספת המוצרים כ-Query Params
    products.forEach(product => {
        queryParams.append(`products[${product.name}]`, product.quantity);
    });

    // שליחה עם ה-Query Params ב-URL
    fetch(`/Grocery/OrderingGoods?${queryParams.toString()}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(company) 
    })
        .then(response => {
            if (!response.ok) throw new Error("שגיאה בשליחה");
            return response.json();
        })
        .then(data => {
            alert("ההזמנה נשלחה בהצלחה!");
        })
        .catch(error => {
            console.error("שגיאה:", error);
            alert("אירעה שגיאה בשליחת ההזמנה.");
        });
}

async function getOrders() {
    const response = await fetch(`${baseUrl}/GetAllOrders`);
    const orders = await response.json();

  
    const ordersList = document.getElementById("ordersList");

    if (ordersList.innerHTML !== "") {
        ordersList.innerHTML = "";
        return;
    }
    ordersList.innerHTML = "";

    if (orders.length === 0) {
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
async function getWaitingOrders() {
    const response = await fetch(`${baseUrl}/GetAllWaitingOrders`);
    const orders = await response.json();
  

    const ordersList = document.getElementById("waitingOrdersList");
    if (ordersList.innerHTML !== "") {
        ordersList.innerHTML = "";
        return;
    }
    ordersList.innerHTML = "";

    if (orders.length === 0) {
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
//async function confirmReceipt() {
//    const orderNum = document.getElementById("confirmOrderNum").value;
//    await fetch(`${baseUrl}/ConfirmationReceipOrder?orderNum=${orderNum}`);
//    alert("קבלה אושרה");
//}

async function completeOrder() {
    const orderNum = document.getElementById("completeOrderNum").value;
    await fetch(`${baseUrl}/OrdeCompletionConfirmation`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(parseInt(orderNum))
    });
    alert("הזמנה הושלמה");
}
