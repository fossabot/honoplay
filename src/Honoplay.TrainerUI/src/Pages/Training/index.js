import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Button } from "../../Components/Button";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import {
  fetchTraineeList,
  fetchTrainingList
} from "@omegabigdata/honoplay-redux-helper/Src/actions/TrainerUser";
import WithAuth from "../../Hoc/CheckAuth";
import {
  MyTrainings,
  TrainingDate,
  TrainingName,
  TrainingDescription,
  Surname,
  MobilePhone,
  Email,
  IsLogin,
  JoinCode,
  StartGame,
  Name,
  TraineeList
} from "../../Helpers/TerasuKeys";
import { translate } from "@omegabigdata/terasu-api-proxy";

class Training extends Component {
  componentDidMount() {
    const selectedClassroomId = localStorage.getItem("selectedClassroom");
    if (selectedClassroomId) {
      this.props.fetchTraineeList(selectedClassroomId);
      this.props.fetchTrainingList();
    }
  }

  render() {
    const selectedTrainingId = localStorage.getItem("selectedTraining");

    if (!this.props.trainingList) {
      return null;
    }

    let selectedtraining = this.props.trainingList.items.filter(
      q => q.id == selectedTrainingId
    )[0];

    return (
      <PageWrapper {...this.state} boxNumber="2">
        <div className="col-sm-12">
          <nav aria-label="breadcrumb">
            <ol className="breadcrumb">
              <li className="breadcrumb-item">
                <Link to="homepage">{translate(MyTrainings)}</Link>
              </li>
              <li className="breadcrumb-item active" aria-current="page">
                {selectedtraining.name}
              </li>
            </ol>
          </nav>

          <table className="table">
            <thead className="thead-light">
              <tr>
                <th scope="col">{translate(TrainingDate)}</th>
                <th scope="col">{translate(TrainingName)}</th>
                <th scope="col">{translate(TrainingDescription)}</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td> {selectedtraining.beginDateTime.formatDate()}</td>
                <td> {selectedtraining.name}</td>
                <td> {selectedtraining.description}</td>
              </tr>
            </tbody>
          </table>

          <div className="mt-5">
            {/* // ToDo : Alttaki bilgiler singlR `dan gelecek  (Emre)  */}
            {/* <p className="font-weight-bold">Kullanıcı Login Olma Oranı</p>
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
            </div> */}

            <h4 className="font-weight-bold text-primary mb-4">
              {translate(TraineeList)} (21 Adet)
            </h4>
            <table className="table">
              <thead className="thead-light">
                <tr>
                  <th scope="col">{translate(Name)}</th>
                  <th scope="col">{translate(Surname)}</th>
                  <th scope="col">{translate(MobilePhone)}</th>
                  <th scope="col">{translate(Email)}</th>
                  <th scope="col">{translate(IsLogin)}</th>
                </tr>
              </thead>
              <tbody>
                {this.props.traineeList &&
                  this.props.traineeList.items.map(t => {
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

            {/* Pagination */}
            {/* <nav aria-label="Page navigation example">
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
            </nav> */}

            <div className="mt-5">
              <Link
                target="about_blank"
                to="/joincode"
                className="btn btn-primary mr-2"
              >
                {translate(JoinCode)}
              </Link>

              <Button title={translate(StartGame)} />
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
