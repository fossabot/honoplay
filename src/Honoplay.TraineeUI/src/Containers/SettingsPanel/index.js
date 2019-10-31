import React from "react";
import DefaultAvatar from "../../Assets/img/avatar.jpg";
import History from "../../Helpers/History";
import { GetImage } from "../../Helpers/Image";
import Image from "react-image-webp";

class SettingsPanel extends React.Component {
  render() {
    const userData = JSON.parse(localStorage.getItem("traineeUserData"));
    console.log("user data :", userData);

    return (
      <div className="top p-3 shadow">
        <div className="float-left">
          <table className="w-100">
            <tbody>
              <tr>
                <td style={{ width: "70px" }}>
                  <figure className="avatar-profile mb-0">
                    {/* <Image src={this.state.image} webp={this.state.image} /> */}
                    <img
                      async="on"
                      decoding="async"
                      src={GetImage("125")}
                      className="img-fluid shadow-sm"
                    />
                  </figure>
                </td>
                <td>
                  <p
                    style={{ marginLeft: "8px" }}
                    className="font-weight-bold mb-0"
                  >
                    {userData && userData.name + " " + userData.surname}
                  </p>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <div className="float-right pt-3">
          <a
            onClick={() => {
              History.push("/settings");
            }}
          >
            <div className="btn my-btn2">
              <i className="fas fa-cog"></i> Kullanıcı ve Şifre Ayarları
            </div>
          </a>
        </div>
        <div className="clearfix"></div>
      </div>
    );
  }
}

export default SettingsPanel;
