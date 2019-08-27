import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Logo, Vector } from "../../Assets/index";
import { Link } from "react-router-dom";
import { TrainerList } from "../../Helpers/DummyData";

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
              {TrainerList.map(data => {
                return (
                  <tr>
                    <td>{data.TrainerDate}</td>
                    <td>{data.TrainerName}</td>
                    <td>{data.Location}</td>
                    <td>
                      <Link to="/trainingdetail">İncele</Link>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
      </PageWrapper>
    );
  }
}

export default Home;
