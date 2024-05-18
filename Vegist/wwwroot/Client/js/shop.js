const shopBasket = document.querySelectorAll(".shopBasket")
const productModal = document.querySelector(".productModal")

const overlay = document.querySelector(".overlay");
const closeModalBtn = document.querySelector("#close-search i");

const shopFilter = document.querySelector(".shopFilter");
const shopLeft = document.querySelector(".shop-left");
const closeIcon = document.querySelector(".closeIcon i");



const openModal = function () {
    productModal.classList.remove("hidden");
    overlay.classList.remove("hidden");
};

shopBasket.forEach((basket) => {
    basket.addEventListener("click", openModal);
})

const closeModal = function () {
    productModal.classList.add("hidden");
    overlay.classList.add("hidden");
};

closeModalBtn.addEventListener("click", closeModal);

overlay.addEventListener("click", closeModal);


shopFilter.addEventListener("click", () => {
    shopLeft.style.display = "block";
    document.body.style.overflow = 'hidden';
    overlay.classList.remove("hidden");
})

closeIcon.addEventListener("click", () => {
    shopLeft.style.display = "none";
    document.body.style.overflow = 'auto';
    overlay.classList.add("hidden");
})

document.addEventListener('keydown', function (event) {
    const key = event.key;
    if (key === "Escape") {
        shopLeft.style.display = "none";
        overlay.classList.add("hidden");
    }
});