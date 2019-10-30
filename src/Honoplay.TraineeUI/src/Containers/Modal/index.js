import React from "react";

class Modal extends React.Component {
  render() {
    return (
      <div
        class="modal fade bd-example-modal-xl"
        tabindex="-1"
        role="dialog"
        aria-labelledby="myExtraLargeModalLabel"
        aria-hidden="true"
      >
        <div class="modal-dialog modal-xl">
          <div class="modal-content">
            <div class="modal-header">
              <h5
                class="modal-title font-weight-bold text-primary"
                id="exampleModalLabel"
              >
                Aşağıdan Avatarınızı Seçiniz
              </h5>
              <button
                type="button"
                class="close"
                data-dismiss="modal"
                aria-label="Close"
              >
                <span aria-hidden="true">&times;</span>
              </button>
            </div>
            <div class="modal-body">
              <div class="row">
                {this.props.Images &&
                  this.props.Images.items.map(img => {
                    return (
                      <div class="col-sm-2 mb-4">
                        <img
                          src={`data:image/jpeg;base64,${img.imageBytes}`}
                          class="img-thumbnail"
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
