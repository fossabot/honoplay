import React from 'react';
import {
  Dialog,
  Slide,
  DialogContent,
  DialogTitle
} from '@material-ui/core';

function Transition(props) {
  return <Slide direction="up" {...props} />;
}

class ModalComponent extends React.Component {

  render() {
    const { open, handleClose, children, titleName } = this.props;
    return (

      <div>
        <Dialog
          fullWidth
          maxWidth='lg'
          open={open}
          onClose={handleClose}
          TransitionComponent={Transition}
        >
          <DialogTitle>{titleName}</DialogTitle>
          <DialogContent>
            {children}
          </DialogContent>
        </Dialog>
      </div>
    );
  }
}

export default ModalComponent;
