import React from 'react';
import {Button, InputBase, Grid, InputLabel} from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles'; 
import { Style } from './Style';

class FileInputComponent extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        filename: '',
      }
    }
    handleImageName(event) {
        this.setState({ 
          filename: event.target.value.replace("C:\\fakepath\\", "")
        });
    }
  
    render() {
      const { classes, labelName } = this.props;
     
      return (
        <div className={classes.inputRoot}>
          <Grid container spacing={24}>
            {(labelName && 
                <Grid item xs={12} sm={3}
                      className={classes.labelCenter}>
                    <InputLabel  className={classes.bootstrapFormLabel}>
                        {labelName}
                    </InputLabel>                                   
                </Grid> 
            )}
            <Grid item xs={8} sm={5}>
                <InputBase classes={{
                                root: classes.bootstrapRoot,
                                input: classes.bootstrapInput,
                            }}
                            value={this.state.filename}   
                            fullWidth               
                />   
            </Grid>
            <Grid item xs={4} sm={4}>
                <input accept="image/*"
                       className={classes.fileInput}
                       id="button-file"
                       multiple
                       type="file"               
                       onChange={this.handleImageName.bind(this)}
                />
                <label htmlFor="button-file" >
                    <Button variant="outlined" 
                            component="span" 
                            className={classes.fileInputButton}>
                    Browse
                    </Button>
                </label>              
            </Grid>   
          </Grid>
        </div>
      );
    }
  }

export default withStyles(Style)(FileInputComponent);
  