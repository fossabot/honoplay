import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Logo, Vector } from "../../Assets/index";

class Home extends Component {
  render() {
    return (
      <PageWrapper {...this.state} boxNumber="2">
        <div className="col-sm-12">
          <nav aria-label="breadcrumb">
            <ol className="breadcrumb">
              <li className="breadcrumb-item">
                <a href="egitmen-anasayfa.html">Eğitimler</a>
              </li>
              <li className="breadcrumb-item active" aria-current="page">
                Satış ve Pazarlama Eğitimi
              </li>
            </ol>
          </nav>

          <table className="table">
            <thead className="thead-light">
              <tr>
                <th scope="col">Eğitim Tarihi</th>
                <th scope="col">Eğitim Adı</th>
                <th scope="col">Eğitim Yeri</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>01.07.2019</td>
                <td>Satış ve Pazarlama Eğitimi</td>
                <td>Lütfü Kırdar Gösteri Merkezi</td>
              </tr>
            </tbody>
          </table>

          <div className="mt-5">
            <p className="font-weight-bold">Kullanıcı Login Olma Oranı</p>
            <div className="progress mb-5" style={{ height: 30 }}>
              <div
                className="progress-bar"
                role="progressbar"
                style={{ width: 25 }}
                aria-valuenow="25"
                aria-valuemin="0"
                aria-valuemax="100"
              >
                5 / 21
              </div>
            </div>

            <h4 className="font-weight-bold text-primary mb-4">
              Kullanıcı Listesi (21 Adet)
            </h4>
            <table className="table">
              <thead className="thead-light">
                <tr>
                  <th scope="col">Ad</th>
                  <th scope="col">Soyad</th>
                  <th scope="col">Cep Telefonu</th>
                  <th scope="col">Şirket Adı</th>
                  <th scope="col">Statü</th>
                  <th scope="col">Giriş Yaptı</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td>Ahmet</td>
                  <td>Tunca</td>
                  <td>0000 000 0000</td>
                  <td>Tunca Ltd.</td>
                  <td>CTO</td>
                  <td>
                    <i
                      className="fas fa-check-circle text-success"
                      style={{ fonstSize: 20 }}
                    ></i>
                  </td>
                </tr>
                <tr>
                  <td>Ahmet</td>
                  <td>Tunca</td>
                  <td>0000 000 0000</td>
                  <td>Tunca Ltd.</td>
                  <td>CTO</td>
                  <td>
                    <i
                      className="fas fa-times-circle text-secondary"
                      style={{ fonstSize: 22 }}
                    ></i>
                  </td>
                </tr>
                <tr>
                  <td>Ahmet</td>
                  <td>Tunca</td>
                  <td>0000 000 0000</td>
                  <td>Tunca Ltd.</td>
                  <td>CTO</td>
                  <td>
                    <i
                      className="fas fa-times-circle text-secondary"
                      style={{ fonstSize: 22 }}
                    ></i>
                  </td>
                </tr>
                <tr>
                  <td>Ahmet</td>
                  <td>Tunca</td>
                  <td>0000 000 0000</td>
                  <td>Tunca Ltd.</td>
                  <td>CTO</td>
                  <td>
                    <i
                      className="fas fa-check-circle text-success"
                      style={{ fonstSize: "22%" }}
                    ></i>
                  </td>
                </tr>
              </tbody>
            </table>

            <nav aria-label="Page navigation example">
              <ul className="pagination">
                <li className="page-item">
                  <a className="page-link" href="#" aria-label="Previous">
                    <span aria-hidden="true">&laquo;</span>
                  </a>
                </li>
                <li className="page-item">
                  <a className="page-link" href="#">
                    1
                  </a>
                </li>
                <li className="page-item">
                  <a className="page-link" href="#">
                    2
                  </a>
                </li>
                <li className="page-item">
                  <a className="page-link" href="#">
                    3
                  </a>
                </li>
                <li className="page-item">
                  <a className="page-link" href="#" aria-label="Next">
                    <span aria-hidden="true">&raquo;</span>
                  </a>
                </li>
              </ul>
            </nav>

            <div className="mt-5">
              <a
                href="katilim-kodu.html"
                target="_blank"
                className="btn btn-primary mr-2"
              >
                Katılım Kodunu Göster
              </a>
              <button type="button" className="btn btn-success">
                Oyunu Başlat
              </button>
            </div>
          </div>
        </div>
      </PageWrapper>
    );
  }
}

export default Home;
