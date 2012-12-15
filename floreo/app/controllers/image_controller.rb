require "imageruby"
class ImageController < ApplicationController
	include ImageRuby
	def index
	end
	
	def apply_filter
		content = params[:file_content][:content].read
		
		uuid = UUID.new
		generated_uuid = uuid.generate
		path = "/tmp" + "/" + generated_uuid
		
		Dir::mkdir(path)
		
		file_ext = File.extname(params[:file_content][:content].original_filename)
		
		File.open(path + "/file_name" + file_ext,"w") do |f|
			f.write(content.force_encoding("UTF-8"))
		end 		
		fullpath = path + "/file_name" + file_ext
		image = Sorcery.new(fullpath)
		p image.identify 
		image.manipulate!(sharpen: "100") # => true
		#~ image.dimensions # => { x: 250, y: 250 }
		image.convert(path + "/file_name2." + file_ext, quality: 80, sharpen: "100") # => true
		 
	end
end
