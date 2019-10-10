import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { connect } from "react-redux";
import { fetchClassroomList } from "@omegabigdata/honoplay-redux-helper/Src/actions/TrainerUser";
import { Link } from "react-router-dom";
import WithAuth from "../../Hoc/CheckAuth";
import {
  MyClassroom,
  ClassroomName,
  ShowTrainee,
  Examined
} from "../../Helpers/TerasuKeys";

class Classroom extends Component {
  componentDidMount() {
    const { location } = this.props;
    if (!location.state) {
      History.goBack();
    } else {
      this.props.fetchClassroomList(location.state);
    }
  }
  render() {
    return (
      <PageWrapper>
        <div className="col-sm-12">
          <h4 className="font-weight-bold text-primary mb-4">{MyClassroom}</h4>
          <table className="table">
            <thead className="thead-light">
              <tr>
                <th scope="col">{ClassroomName}</th>
                <th scope="col">{ShowTrainee}</th>
              </tr>
            </thead>
            <tbody>
              {this.props.classroomList &&
                this.props.classroomList.items.map((data, index) => {
                  return (
                    <tr key={index}>
                      <td>{data.name}</td>
                      <td>
                        <Link
                          to={{
                            pathname: "/trainer/trainingdetail",
                            state: data.id
                          }}
                        >
                          {Examined}
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
    isClassroomIsLoading,
    classroomList,
    errorClassroomListError
  } = state.userTrainerClassroomList;

  return { isClassroomIsLoading, classroomList, errorClassroomListError };
};

export default connect(
  mapStateToProps,
  { fetchClassroomList }
)(WithAuth(Classroom));
