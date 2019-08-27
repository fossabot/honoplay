import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Logo, Vector } from "../../Assets/index";

class Home extends Component {
  render() {
    return (
      <PageWrapper {...this.state} boxNumber="2">
        <div class="col-sm-12">
          <h4 class="font-weight-bold text-primary mb-4">Eğitimlerim</h4>
          <table class="table">
            <thead class="thead-light">
              <tr>
                <th scope="col">Eğitim Tarihi</th>
                <th scope="col">Eğitim Adı</th>
                <th scope="col">Eğitim Yeri</th>
                <th scope="col">Detay</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>01.07.2019</td>
                <td>Satış ve Pazarlama Eğitimi</td>
                <td>Lütfü Kırdar Gösteri Merkezi</td>
                <td>
                  <a href="#">İncele</a>
                </td>
              </tr>
              <tr>
                <td>01.07.2019</td>
                <td>Satış ve Pazarlama Eğitimi</td>
                <td>Lütfü Kırdar Gösteri Merkezi</td>
                <td>
                  <a href="#">İncele</a>
                </td>
              </tr>
              <tr>
                <td>01.07.2019</td>
                <td>Satış ve Pazarlama Eğitimi</td>
                <td>Lütfü Kırdar Gösteri Merkezi</td>
                <td>
                  <a href="#">İncele</a>
                </td>
              </tr>
              <tr>
                <td>01.07.2019</td>
                <td>Satış ve Pazarlama Eğitimi</td>
                <td>Lütfü Kırdar Gösteri Merkezi</td>
                <td>
                  <a href="#">İncele</a>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </PageWrapper>
    );
  }
}

export default Home;
