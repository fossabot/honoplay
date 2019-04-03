import React from 'react';
import {Button, InputBase, Grid, InputLabel} from '@material-ui/core';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles'; 
import Style from './Style';

class FileInputComponent extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        filename: ''
      }
      this.fileInput = React.createRef();
    }
    handleImageName(e) {
        this.setState({ filename:  this.fileInput.current.files[0].name });
    }
  
    render() {
      const { classes, LabelName } = this.props;

      return (
        <div className={classes.root}>
          <Grid container spacing={24}>
            {(LabelName ? 
                <Grid item xs={3} className={classes.center}>
                    <InputLabel  className={classes.bootstrapFormLabel}>
                        {LabelName}
                    </InputLabel>                                   
                </Grid> : ""
            )}
            <Grid item xs={6} sm={7}>
                <InputBase classes={{
                                root: classes.bootstrapRoot,
                                input: classes.bootstrapInput,
                            }}
                            value={this.state.filename}   
                            fullWidth               
                />   
            </Grid>
            <Grid item xs={3} sm={2}>
                <input accept="image/*"
                       className={classes.input}
                       id="outlined-button-file"
                       multiple
                       type="file"               
                       onChange={this.handleImageName.bind(this)}
                       ref={this.fileInput}
                />
                <label htmlFor="outlined-button-file" >
                    <Button variant="outlined" component="span" onClick={this.handleImageName.bind(this)} className={classes.button}>
                    Browse
                    </Button>
                </label>              
            </Grid>
          
          </Grid>
        </div>
      );
    }
  }
  
FileInputComponent.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(Style)(FileInputComponent);
  