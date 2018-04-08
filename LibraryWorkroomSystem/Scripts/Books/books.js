//this script tests to make sure the login functionality doesn't work with empty inputs
function validateSearch() {
	if (String(searchData.SearchBox.value).toString().includes(";")) {
		window.alert("Invalid Search.");
		return false;
	} 
    return true;
	
}

//this script tests to make sure the program creation information is valid
function checkBookData() {
	if (bookData.title.value === "" || bookData.author.value === ""
		|| bookData.publish_date.value === "" || bookData.floor.value === "") {
		window.alert("All boxes must be filled except for series.");
		return false;
	}

	return true;
}