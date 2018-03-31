


//this script tests to make sure the login functionality doesn't work with empty inputs
function validateCredentials() {
	if (credentials.password.value === "" || credentials.username === "") {
		return false;
	} else {
		return true;
	}
}

//this script tests to make sure the account creation information is valid
function checkAccountData() {
	if (accountData.name.value === "" || accountData.username.value === ""
		|| accountData.password.value === "" || accountData.confirmPassword === "") {
		window.alert("Must Specify All Boxes.");
		return false;
	}

	if (accountData.password.value !== accountData.confirmPassword.value) {
		window.alert("Passwords do not match!");
		return false;
	}
	return true;
}