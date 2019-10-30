import React from "react";

class Modal extends React.Component {
  render() {
    return (
      <div
        className="modal fade bd-example-modal-xl"
        tabindex="-1"
        role="dialog"
        aria-labelledby="myExtraLargeModalLabel"
        aria-hidden="true"
      >
        <div className="modal-dialog modal-xl">
          <div className="modal-content">
            <div className="modal-header">
              <h5
                className="modal-title font-weight-bold text-primary"
                id="exampleModalLabel"
              >
                Aşağıdan Avatarınızı Seçiniz
              </h5>
              <button
                type="button"
                className="close"
                data-dismiss="modal"
                aria-label="Close"
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div className="modal-body">
              <div className="row">
                {/* {this.props.Images &&
                  this.props.Images.items.map(img => {
                    return (
                      <div className="col-sm-2 mb-4">
                        <img
                          src={`data:image/jpeg;base64,${img.imageBytes}`}
                          className="img-thumbnail"
                        />
   
                      </div>
                    );
                  })} */}
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default Modal;
