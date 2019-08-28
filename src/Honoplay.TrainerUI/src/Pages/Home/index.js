import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Logo, Vector } from "../../Assets/index";
import { Link } from "react-router-dom";
import { TrainerList } from "../../Helpers/DummyData";
import WithAuth from "../../Hoc/CheckAuth";
import { connect } from "react-redux";
import { fetchTrainingList } from "@omegabigdata/honoplay-redux-helper/Src/actions/TrainerUser";

class Home extends Component {
  componentDidMount() {
    this.props.fetchTrainingList();
  }

  componentDidUpdate(prevProps) {
    console.log(this.props.trainingList);
  }

  render() {
    return (
      <PageWrapper {...this.state} boxNumber="2">
        <div className="col-sm-12">
          <h4 className="font-weight-bold text-primary mb-4">Eğitimlerim</h4>
          <table className="table">
            <thead className="thead-light">
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
                  <tr key={data.Id}>
                    <td>{data.TrainerDate}</td>
                    <td>{data.TrainerName}</td>
                    <td>{data.Location}</td>
                    <td>
                      <Link to={{ pathname: "/trainingdetail", state: data }}>
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
)(Home);
