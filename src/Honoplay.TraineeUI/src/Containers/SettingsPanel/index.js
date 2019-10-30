import React from "react";
import DefaultAvatar from "../../Assets/img/avatar.jpg";
import History from "../../Helpers/History";

const userData = JSON.parse(localStorage.getItem("traineeUserData"));
class Settings extends React.Component {
  render() {
    return (
      <div className="top p-3 shadow">
        <div className="float-left">
          <table className="w-100">
            <tbody>
              <tr>
                <td style={{ width: "70px" }}>
                  <figure className="avatar-profile mb-0">
                    {this.props.ImageByte ? (
                      <img
                        src={`data:image/jpeg;base64,${this.props.ImageByte.items[0].imageBytes}`}
                        className="img-fluid shadow-sm"
                      />
                    ) : (
                      <img
                        src={DefaultAvatar}
                        className="img-fluid shadow-sm"
                      />
                    )}
                  </figure>
                </td>
                <td>
                  <p
                    style={{ marginLeft: "8px" }}
                    className="font-weight-bold mb-0"
                  >
                    {userData.name} {userData.surname}
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

export default Settings;
