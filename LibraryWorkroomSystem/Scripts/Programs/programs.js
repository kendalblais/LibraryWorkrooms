

//this script tests to make sure the program creation information is valid
function checkProgramData() {
	if (programData.name.value === "" || programData.description.value === ""
		|| programData.date.value === "" || programData.startTime === ""
		|| programData.endTime === "" || programData.teacherID === "") {
		window.alert("All Boxes Must be Filled.");
		return false;
	}

	var date = /^\d\d\d\d-\d\d-\d\d$/
	if (!date.test(programData.date.value)) {
		window.alert("Date must be in the form of yyyy-MM-dd");
		return false;
	}

	var time = /^(\d)?\d:\d\d:\d\d$/
	if (!time.test(String(programData.startTime.value))) {
		window.alert("Time must be in the form of hh:mm:ss");
		return false;
	}

    //I think there was a bug here. Change from date.test to time.test
	if (!time.test(String(programData.endTime.value))) {
		window.alert("Time must be in the form of hh:mm:ss");
		return false;
	}


	return true;
}