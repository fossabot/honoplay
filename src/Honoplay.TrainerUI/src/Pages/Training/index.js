import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Button } from "../../Components/Button";
import { Link } from "react-router-dom";
import History from "../../Helpers/History";
import { connect } from "react-redux";
import {
  fetchTraineeList,
  fetchTrainingList
} from "@omegabigdata/honoplay-redux-helper/Src/actions/TrainerUser";
import WithAuth from "../../Hoc/CheckAuth";

class Training extends Component {
  componentDidMount() {
    const { location } = this.props;
    if (!location.state) {
      History.goBack();
    }
    this.props.fetchTraineeList(location.state);
  }

  render() {
    const selectedTrainingId = localStorage.getItem("selectedTraining");

    const selectedtraining = this.props.trainingList.items.filter(
      q => q.id == selectedTrainingId
    )[0];

    return (
      <PageWrapper {...this.state} boxNumber="2">
        <div className="col-sm-12">
          <nav aria-label="breadcrumb">
            <ol className="breadcrumb">
              <li className="breadcrumb-item">
                <Link to="homepage">Eğitimler</Link>
              </li>
              <li className="breadcrumb-item active" aria-current="page">
                {selectedtraining.name}
              </li>
            </ol>
          </nav>

          <table className="table">
            <thead className="thead-light">
              <tr>
                <th scope="col">Eğitim Tarihi</th>
                <th scope="col">Eğitim Adı</th>
                <th scope="col">Eğitim Açıklama</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td> {selectedtraining.beginDateTime}</td>
                <td> {selectedtraining.name}</td>
                <td> {selectedtraining.description}</td>
              </tr>
            </tbody>
          </table>

          <div className="mt-5">
            <p className="font-weight-bold">Kullanıcı Login Olma Oranı</p>
            <div className="progress mb-5" style={{ height: 30 }}>
              <div
                className="progress-bar"
                role="progressbar"
                style={{ width: "25%" }}
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
                  <th scope="col">Eposta</th>
                  <th scope="col">Giriş Yaptı</th>
                </tr>
              </thead>
              <tbody>
                {[] || this.props.traineeList.items.map(t => {
                  return (
                    <tr key={t.id}>
                      <td>{t.name}</td>
                      <td>{t.surname}</td>
                      <td>{t.phoneNumber}</td>
                      <td>{t.email}</td>
                      <td>
                        <i
                          className={`fas fa-check-circle text-success`}
                          style={{ fonstSize: 22 }}
                        ></i>
                      </td>
                    </tr>
                  );
                })}
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
              <Link
                target="about_blank"
                to="/trainer/joincode"
                className="btn btn-primary mr-2"
              >
                Katılım Kodunu Göster
              </Link>

              <Button title="Oyunu Başlat" />
            </div>
          </div>
        </div>
      </PageWrapper>
    );
  }
}

const mapStateToProps = state => {
  const {
    isTraineeListLoading,
    traineeList,
    errorTraineeListError
  } = state.trainerUserTraineeList;

  const { trainingList } = state.trainerUserTrainingList;

  return {
    isTraineeListLoading,
    traineeList,
    errorTraineeListError,
    trainingList
  };
};

export default connect(
  mapStateToProps,
  { fetchTraineeList, fetchTrainingList }
)(WithAuth(Training));
