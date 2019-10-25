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
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy001.png"
                    class="img-thumbnail"
                  />
                </div>
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy001a.png"
                    class="img-thumbnail"
                  />
                </div>
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy001b.png"
                    class="img-thumbnail"
                  />
                </div>
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy002.png"
                    class="img-thumbnail"
                  />
                </div>
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy002a.png"
                    class="img-thumbnail"
                  />
                </div>
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy002b.png"
                    class="img-thumbnail"
                  />
                </div>
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy003.png"
                    class="img-thumbnail"
                  />
                </div>
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy003a.png"
                    class="img-thumbnail"
                  />
                </div>
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy003b.png"
                    class="img-thumbnail"
                  />
                </div>
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy004.png"
                    class="img-thumbnail"
                  />
                </div>
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy004a.png"
                    class="img-thumbnail"
                  />
                </div>
                <div class="col-sm-2 mb-4">
                  <img
                    src="img/avatars/pipo-enemy004b.png"
                    class="img-thumbnail"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default Modal;
