import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { fetchTrainingList } from "@omegabigdata/honoplay-redux-helper/Src/actions/TrainerUser";
import WithAuth from "../../Hoc/CheckAuth";
import {
  MyTrainings,
  TrainingDate,
  TrainingName,
  TrainingDescription,
  Detail,
  Examined
} from "../../Helpers/TerasuKeys";
import { translate } from "@omegabigdata/terasu-api-proxy";

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
          <h4 className="font-weight-bold text-primary mb-4">
            {translate(MyTrainings)}
          </h4>
          <table className="table">
            <thead className="thead-light">
              <tr>
                <th scope="col">{translate(TrainingDate)}</th>
                <th scope="col">{translate(TrainingName)}</th>
                <th scope="col">{translate(TrainingDescription)}</th>
                <th scope="col">{translate(Detail)}</th>
              </tr>
            </thead>
            <tbody>
              {this.props.trainingList.items.map((data, index) => {
                return (
                  <tr key={index}>
                    <td>{data.beginDateTime.formatDate()}</td>
                    <td>{data.name}</td>
                    <td>{data.description}</td>
                    <td>
                      <Link
                        onClick={() => {
                          localStorage.setItem("selectedTraining", data.id);
                        }}
                        to={{ pathname: "/classroom", state: data.id }}
                      >
                        {translate(Examined)}
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
