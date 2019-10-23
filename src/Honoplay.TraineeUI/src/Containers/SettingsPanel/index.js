import React from "react";
import DefaultAvatar from "../../Assets/img/avatar.jpg";
import History from "../../Helpers/History";

const userData = JSON.parse(localStorage.getItem("traineeUserData"));
class Settings extends React.Component {
  render() {
    return (
      <div class="top p-3 shadow">
        <div class="float-left">
          <table class="w-100">
            <tr>
              <td style={{ width: "70px;" }}>
                <figure class="avatar-profile mb-0">
                  <img src={DefaultAvatar} class="img-fluid shadow-sm" />
                </figure>
              </td>
              <td>
                <p style={{ marginLeft: "8px" }} class="font-weight-bold mb-0">
                  {userData.name} {userData.surname}
                </p>
              </td>
            </tr>
          </table>
        </div>
        <div class="float-right pt-3">
          <a
            onClick={() => {
              History.push("/settings");
            }}
          >
            <div class="btn my-btn2">
              <i class="fas fa-cog"></i> Kullanıcı ve Şifre Ayarları
            </div>
          </a>
        </div>
        <div class="clearfix"></div>
      </div>
    );
  }
}

export default Settings;
