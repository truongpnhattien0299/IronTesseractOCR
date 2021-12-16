import React from "react";
import fetchOcr from "../services/fetchOcr";

const IronOcr = () => {
  const [file, setFile] = React.useState();

  const getBase64 = (file) =>
    new Promise((resolve, reject) => {
      var reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        resolve(reader.result);
      };
      reader.onerror = (error) => {
        console.log("Error: ", error);
        reject(error);
      };
    });

  const _onChange = async (e) => {
    const file = e.target.files[0];

    const formData = new FormData();
    formData.append("file", file);
    fetchOcr.postFile(formData).then((res) => console.log(res));
  };
  return (
    <div>
      <input type="file" name="file" id="file" multiple onChange={_onChange} />
    </div>
  );
};
export default IronOcr;
