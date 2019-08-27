import React, { Component } from "react";
import PageWrapper from "../../Containers/PageWrapper";
import { Logo, Vector } from "../../Assets/index";
import { Button } from "../../Components/Button";
import { Link } from "react-router-dom";
import History from "../../Helpers/History";
import { TraineeList } from "../../Helpers/DummyData";

class Training extends Component {
  state = {
    selectedTraining: {
      TrainerDate: null,
      TrainerName: null,
      Location: null,
      Id: 0
    }
  };
  componentDidMount() {
    const { location } = this.props;

    if (!location.state) {
      History.goBack();
    }
    this.setState({
      selectedTraining: location.state
    });
  }

  render() {
    return (
      <PageWrapper {...this.state} boxNumber="2">
        <div className="col-sm-12">
          <nav aria-label="breadcrumb">
            <ol className="breadcrumb">
              <li className="breadcrumb-item">
                <Link to="homepage">Eğitimler</Link>
              </li>
              <li className="breadcrumb-item active" aria-current="page">
                {this.state.selectedTraining.TrainerName}
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
                <td> {this.state.selectedTraining.TrainerDate}</td>
                <td> {this.state.selectedTraining.TrainerName}</td>
                <td> {this.state.selectedTraining.Location}</td>
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
                  <th scope="col">Şirket Adı</th>
                  <th scope="col">Statü</th>
                  <th scope="col">Giriş Yaptı</th>
                </tr>
              </thead>
              <tbody>
                {TraineeList.filter(
                  q => q.TraineId == this.state.selectedTraining.Id
                ).map(t => {
                  return (
                    <tr>
                      <td>{t.Name}</td>
                      <td>{t.Surname}</td>
                      <td>{t.PhoneNumber}</td>
                      <td>{t.TenantName}</td>
                      <td>{t.Role}</td>
                      <td>
                        <i
                          className={`fas fa-check-circle text-${
                            t.IsLogin ? "success" : "secondary"
                          }`}
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
                target="new_blank"
                to="/joincode"
                className="btn btn-primary mr-2"
              >
                Katılım Kodunu Göster
              </Link>
              {/* <Button title="Katılım Kodunu Göster" color={"primary mr-2"} /> */}

              <Button title="Oyunu Başlat" />
            </div>
          </div>
        </div>
      </PageWrapper>
    );
  }
}

export default Training;
