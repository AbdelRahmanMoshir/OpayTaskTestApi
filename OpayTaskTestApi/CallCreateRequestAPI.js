function AddRequest() {
    var url = "https://sandboxapi.opaycheckout.com/api/v1/international/cashier/create";
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            alert(this.responseText);
        }
    };
    xhttp.open("POST", url, true);
    xhttp.setRequestHeader("Content-type", "application/json");
    xhttp.setRequestHeader("Authorization", "Bearer OPAYPUB16388855997950.39843277853359504");
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.send(`{
"request":{   "country" : "EG",
              "reference" : "12312321324",
              "amount": {
                    "total": "400",
                    "currency": "EGP"
                        },
              "payMethod": "BankCard",
              "product": {
                    "name": "Iphone X",
                    "description": "xxxxxxxxxxxxxxxxx"
                        },
              "returnUrl"  : "your returnUrl",
              "callbackUrl"  : "your callbackUrl",
              "cancelUrl"  : "your cancelUrl",
              "userClientIP" : "1.1.1.1",
              "expireAt" : "30"
       }
} `);
}
