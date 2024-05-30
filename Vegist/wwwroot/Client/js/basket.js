const plusCount = document.querySelector(".plusCount");
const minusCount = document.querySelector(".minusCount");
const counter = document.querySelector(".counter");

let count = 1;
counter.value = count;

plusCount.addEventListener("click", () => {
  if (count < 10) {
    count++;
  }
  counter.value = count;

  if(count === 10){
    plusCount.style.cursor = "no-drop";
  }
})

minusCount.addEventListener("click", () => {
  if (count > 1) {
    count--;
  }
  counter.value = count;

  if(count === 1){
    minusCount.style.cursor = "no-drop";
  }
})



///////////////////////////

// const basketListLi = document.querySelector(".basketListLi");
// const subtotal = document.querySelector(".item-total p");
// const discPrice = document.querySelector(".discPrice");


// let totalPrice = 0;
// let count1 = counter.value

// totalPrice += (basketListLi.discPrice * basketListLi.count1);
// subtotal.textContent = `${totalPrice}`;