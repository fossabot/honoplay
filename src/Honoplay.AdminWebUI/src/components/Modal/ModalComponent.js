import React from 'react';
import {
  Dialog,
  Slide,
  DialogContent,
  DialogTitle
} from '@material-ui/core';

const Transition = React.forwardRef((props, ref) =>  <Slide direction="up" {...props} ref={ref}/>);

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
          {titleName &&
            <DialogTitle>{titleName}</DialogTitle>
          }
          <DialogContent>
            {children}
          </DialogContent>
        </Dialog>
      </div>
    );
  }
}

export default ModalComponent;
