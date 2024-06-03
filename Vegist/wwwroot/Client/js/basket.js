document.addEventListener('DOMContentLoaded', function () {
  const plusButtons = document.querySelectorAll('.plusCount');
  const minusButtons = document.querySelectorAll('.minusCount');
  const removeButtons = document.querySelectorAll('.removeBtn');


  function updateTotalPrice(item) {
    const quantityInput = item.querySelector('.counter');
    const discountPrice = parseFloat(item.querySelector('.discPrice').textContent.replace('€', ''));
    const totalPriceElement = item.querySelector('.item-total p');

    const newTotalPrice = (quantityInput.value * discountPrice).toFixed(2);
    totalPriceElement.textContent = `€${newTotalPrice}`;
  }

  function updateButtonStyles(item) {
    const quantityInput = item.querySelector('.counter');
    const plusButton = item.querySelector('.plusCount');
    const minusButton = item.querySelector('.minusCount');
    const quantity = parseInt(quantityInput.value);

    if (quantity >= 10) {
      plusButton.style.cursor = 'no-drop';
    } else {
      plusButton.style.cursor = 'pointer';
    }

    if (quantity <= 1) {
      minusButton.style.cursor = 'no-drop';
    } else {
      minusButton.style.cursor = 'pointer';
    }
  }

  function handleQuantityChange(event) {
    const button = event.target;
    const item = button.closest('.basketListLi');
    const quantityInput = item.querySelector('.counter');
    let quantity = parseInt(quantityInput.value);

    if (button.classList.contains('plusCount') && quantity < 10) {
      quantity += 1;
    } else if (button.classList.contains('minusCount') && quantity > 1) {
      quantity -= 1;
    }

    quantityInput.value = quantity;
    updateTotalPrice(item);
    updateButtonStyles(item);
    updateGrandTotal();
  }

  // Remove
  function handleItemRemove(event) {
    const button = event.target;
    const item = button.closest('.basketListLi');
    item.remove();
    updateGrandTotal();
  }

  function updateGrandTotal() {
    const allItems = document.querySelectorAll('.basketListLi');
    let grandTotal = 0;

    allItems.forEach(item => {
      const totalPrice = parseFloat(item.querySelector('.item-total p').textContent.replace('€', ''));
      grandTotal += totalPrice;
    });

    const grandTotalElement = document.querySelector('.grandTotal');
    if (grandTotalElement) {
      grandTotalElement.textContent = `Grand Total: €${grandTotal.toFixed(2)}`;
    }
  }

  plusButtons.forEach(button => button.addEventListener('click', handleQuantityChange));
  minusButtons.forEach(button => button.addEventListener('click', handleQuantityChange));
  removeButtons.forEach(button => button.addEventListener('click', handleItemRemove));

  updateGrandTotal();
});