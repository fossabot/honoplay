import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { fetchTrainingList } from "@omegabigdata/honoplay-redux-helper/Src/actions/TrainerUser";
import WithAuth from "../../Hoc/CheckAuth";

class Home extends Component {
  componentDidMount() {
    this.props.fetchTrainingList();
  }
  render() {
    if (!this.props.trainingList) {
      return <PageWrapper></PageWrapper>;
    }
    return (
      <PageWrapper {...this.state} boxNumber="2">
        <div className="col-sm-12">
          <h4 className="font-weight-bold text-primary mb-4">Eğitimlerim</h4>
          <table className="table">
            <thead className="thead-light">
              <tr>
                <th scope="col">Eğitim Tarihi</th>
                <th scope="col">Eğitim Adı</th>
                <th scope="col">Aciklama</th>
                <th scope="col">Detay</th>
              </tr>
            </thead>
            <tbody>
              {this.props.trainingList.items.map((data, index) => {
                return (
                  <tr key={index}>
                    <td>{data.beginDateTime}</td>
                    <td>{data.name}</td>
                    <td>{data.description}</td>
                    <td>
                      <Link
                        onClick={() => {
                          console.log("Tiklanan data :", data.id);
                          localStorage.setItem("selectedTraining", data.id);
                        }}
                        to={{ pathname: "/classroom", state: data.id }}
                      >
                        İncele
                      </Link>
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

const mapStateToProps = state => {
  const {
    isTrainingListLoading,
    trainingList,
    errorTrainingList
  } = state.trainerUserTrainingList;

  return { isTrainingListLoading, trainingList, errorTrainingList };
};
export default connect(
  mapStateToProps,
  { fetchTrainingList }
)(WithAuth(Home));
