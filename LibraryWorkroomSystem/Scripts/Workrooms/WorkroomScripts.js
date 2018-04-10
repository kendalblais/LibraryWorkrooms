
//Note: This function isn't 100% correct according to the definition of a leap year.
function validateDate()
{
    if (roomSize.value == 0) {
        window.alert("Please enter a room size greater than zero.");
        return false;
    }
    else if (workroomSearch.month.value == 4 || workroomSearch.month.value == 6 || workroomSearch.month.value == 9 || workroomSearch.month.value == 11) {
        if (workroomSearch.day.value == 31) {
            window.alert("The date you have selected is invalid.");
            return false;
        }
    }
    else if (workroomSearch.month.value == 2) {
        if (workroomSearch.day.value == 31 || workroomSearch.day.value == 30) {
            window.alert("The date you have selected is invalid.");
            return false;
        }
        else if (workroomSearch.day.value == 29 && workroomSearch.year.value % 4 != 0) {
            window.alert("The date you have selected is invalid.");
            return false;
        }
    }
    else {
        return true;
    }
}