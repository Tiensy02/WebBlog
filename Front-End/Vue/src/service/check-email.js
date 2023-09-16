import Axios from "axios";
async function checkEmail(emailCheck ) {
const options = {
  method: 'GET',
  url: 'https://email-checker.p.rapidapi.com/verify/v1',
  params: {
    email: emailCheck
  },
  headers: {
    'X-RapidAPI-Key': '25bddc1bd5msh0ee1a90ce81dd35p1c0ee9jsnba0165cca9a2',
    'X-RapidAPI-Host': 'email-checker.p.rapidapi.com'
  }
};

try {
	const response = await Axios.request(options);
	return response.data
} catch (error) {
	console.error(error);
}
}

export default checkEmail
