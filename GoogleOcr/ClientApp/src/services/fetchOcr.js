import axios from "axios";
const fetchOcr = {
  postFile: (data) => {
    const url = "/home";
    return axios.post(url, data);
  },
};
export default fetchOcr;
