import React from "react";
import { GetImage } from "../../Helpers/Image";

class Modal extends React.Component {
  selectedAvatarId = avatarId => {
    alert(avatarId);
    this.props.selectedAvatarId && this.props.selectedAvatarId(avatarId);
  };

  render() {
    return (
      <div
        className="modal fade bd-example-modal-xl"
        tabIndex="-1"
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
                {this.props.Images &&
                  this.props.Images.items.map((img, index) => {
                    return (
                      <div
                        key={index}
                        style={{ backgroundColor: "red" }}
                        className="col-sm-2 mb-4"
                      >
                        <img
                          onClick={() => this.selectedAvatarId(img.id)}
                          async="on"
                          decoding="async"
                          src={GetImage(img.id)}
                          className="img-thumbnail"
                        />
                      </div>
                    );
                  })}
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default Modal;
