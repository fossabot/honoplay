import React from "react";
import PageWrapper from "../../Containers/PageWrapper";
import WithAuth from "../../Hoc/CheckAuth";
import { Code } from "../../Assets/index";
import { Button } from "../../Components/Button";
import History from "../../Helpers/History";
import {
  Join,
  PleaseEnterYourGameParticipationCode
} from "../../Helpers/TerasuKeys";
import { translate } from "@omegabigdata/terasu-api-proxy";
import SettingsPanel from "../../Containers/SettingsPanel/";
import DefaultAvatar from "../../Assets/img/pipo-enemy001.png";
import Pass from "../../Assets/img/pass.jpg";
import Input from "../../Components/Input";
import Modal from "../../Containers/Modal";
import $ from "jquery";
import "bootstrap/dist/js/bootstrap.bundle";

class Settings extends React.Component {
  render() {
    return (
      <React.Fragment>
        <SettingsPanel />

        <PageWrapper>
          <div class="box shadow p-3">
            <nav aria-label="breadcrumb">
              <ol class="breadcrumb">
                <li class="breadcrumb-item">
                  <a onClick={() => History.goBack()}>
                    <i class="fas fa-arrow-left"></i> Geri Dön
                  </a>
                </li>
              </ol>
            </nav>

            <div class="shadow-sm box">
              <div class="row p-3">
                <div class="col-sm-3">
                  <img src={DefaultAvatar} class="img-thumbnail" />
                </div>
                <div class="col-sm-9">
                  <div class="form">
                    <h5 class="mb-2 font-weight-bold">Avatarı Değiştir</h5>
                    <p>
                      Kazandığın avatarlardan birini seçerek karakterini
                      özelleştirebilirsin.
                    </p>
                    <a
                      // onClick={() => {
                      //   $("#bd-example-modal-xl").modal("show");
                      // }}
                      class="btn my-btn2"
                      data-toggle="modal"
                      data-target=".bd-example-modal-xl"
                    >
                      Yeni Avatar Seç
                    </a>
                  </div>
                </div>
              </div>
            </div>

            <div class="shadow-sm box">
              <div class="row p-3">
                <div class="col-sm-3">
                  <img src={Pass} class="img-fluid" />
                </div>
                <div class="col-sm-9">
                  <div class="form">
                    <h5 class="mb-3 font-weight-bold">Yeni Şifre Oluştur</h5>
                    <Input type="password" placeholder="Yeni Şifre" />
                    <Input type="password" placeholder="Yeni Şifre Tekrar" />
                    <Button
                      className="btn my-btn form-control mt-3"
                      title="Şifreyi Güncelle"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </PageWrapper>
        <Modal />
      </React.Fragment>
    );
  }
}

export default WithAuth(Settings);
