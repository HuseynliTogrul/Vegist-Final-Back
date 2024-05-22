const forgotPasswordA = document.querySelector(".login a")
const resetPasswordA = document.querySelector(".resetPassword a")
const resetPassword = document.querySelector(".resetPassword")
const login = document.querySelector(".login")


forgotPasswordA.addEventListener("click", () => {
    resetPassword.style.display = "flex";
    login.style.display = "none";
})

resetPasswordA.addEventListener("click", () => {
    resetPassword.style.display = "none";
    login.style.display = "flex";
})