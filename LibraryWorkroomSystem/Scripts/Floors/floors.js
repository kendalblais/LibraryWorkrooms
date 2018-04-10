//Function to ensure all required fields are enterred and values are not too large/small.
function validateRoom() {
    if (saveRoom.roomNo == null || saveRoom.size == null) {
        window.alert("Please ensure all fields have a value enterred.");
        return false;
    }
    else if (saveRoom.roomNo.value <= 0 || saveRoom.roomNo.value >= Math.pow(2, 16)) {
        window.alert("Please enter a room number greater than zero and less than 2^16.");
        return false;
    }
    else if (saveRoom.size.value <= 0 || saveRoom.roomNo.value >= Math.pow(2, 16)) {
        window.alert("Please enter a capacity greater than zero and less than 2^16.");
        return false;
    }

    return true;
}