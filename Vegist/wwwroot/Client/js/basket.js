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
})

minusCount.addEventListener("click", () => {
    if (count > 1) {
        count--;
    }
    counter.value = count;
})