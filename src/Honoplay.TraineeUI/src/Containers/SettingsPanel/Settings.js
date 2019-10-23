import React from "react";

class SettingsPanel extends React.Component {
  render() {
    return (
      <div class="top p-3 shadow">
        <div class="float-left">
          <table class="w-100">
            <tr>
              <td
                style={{
                  width: "70px"
                }}
              >
                <figure class="avatar-profile mb-0">
                  {/* <img src="img/avatar.jpg" class="img-fluid shadow-sm" /> */}
                </figure>
              </td>
              <td>
                <p class="font-weight-bold mb-0">Bruce Wayne</p>
              </td>
            </tr>
          </table>
        </div>
        <div class="float-right pt-3">
          <a href="ayarlar.html">
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

export default SettingsPanel;
